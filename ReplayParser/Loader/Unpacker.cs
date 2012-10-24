using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReplayParser.Loader
{
    public class Unpacker
    {
        
	    protected const int IDENTIFIER_LENGTH   = 4;
	    protected const int HEADER_LENGTH       = 633;
        protected const int SECTION_SIZE_LENGTH = 4;

        protected BinaryReader _reader;

        public UnpackResult Unpack(BinaryReader reader)
        {
            _reader = reader;


            UnpackResult result = null;
            try
            {
                byte[] identifier;
                byte[] header;
                byte[] actions;
                byte[] map;

                // unpack the identifier and header
                identifier = UnpackNextSection(IDENTIFIER_LENGTH);
                header = UnpackNextSection(HEADER_LENGTH);

                // unpack the actions section
                int actionsSize = Common.ToInteger(UnpackNextSection(SECTION_SIZE_LENGTH));
                actions = UnpackNextSection(actionsSize);

                // unpack the map section
                int mapSize = Common.ToInteger(UnpackNextSection(SECTION_SIZE_LENGTH));
                map = UnpackNextSection(mapSize);

                result = new UnpackResult(identifier, header, actions, map);

            }
            finally
            {
                reader.Close();
            }

            return result;
        }

        /// <summary>
        /// Unpacks next section of the compressed replay
        /// </summary>
        /// <param name="length">The length of the section</param>
        /// <returns>The decoded data</returns>
        protected byte[] UnpackNextSection(int length)
        {
            byte[] result = new byte[length];

            Decoder decoder = new Decoder();
            DecodeBuffer buffer = new DecodeBuffer(result);

            decoder.DecodeBuffer = buffer;


            int checksum = _reader.ReadInt32();
            int blocks = _reader.ReadInt32();

            for (int block = 0; block < blocks; block++)
            {

                // read the length of the next block of encoded data
                int encodedLength = _reader.ReadInt32();

                // we error if there is no space for the next block of encoded data
                if (encodedLength > result.Length - buffer.ResultOffset)
                {
                    throw new Exception("Insufficient space in decode buffer");
                }

                // read the block of encoded data into the result (decoded data will overwrite).
                _reader.Read(result, buffer.ResultOffset, encodedLength);

                // skip decoding if the encoded data filled the remaining space
                if (encodedLength == Math.Min(result.Length - buffer.ResultOffset, buffer.BufferLength))
                {
                    continue;
                }

                // set the decode buffer parameters
                buffer.EncodedOffset = 0;
                buffer.DecodedLength = 0;
                buffer.EncodedLength = encodedLength;

                // decode the block
                if (decoder.DecodeBlock() != 0)
                {
                    throw new Exception("Error decoding block offset " + block);
                }

                // sanity check the decoded length
                if (buffer.DecodedLength == 0 ||
                        buffer.DecodedLength > buffer.BufferLength ||
                        buffer.DecodedLength > result.Length)
                {
                    throw new Exception("Decode data length mismatch");
                }

                // flush the decoded bytes into the result
                buffer.WriteDecodedBytes();
            }

            return result;
        }
    }
}
