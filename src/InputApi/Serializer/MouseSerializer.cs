using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputApi.Mouse;

namespace InputApi.Serializer
{
    public class MouseSerializer
    {
        private MouseInput[] _Inputs;
        private byte[] _Bytes;

        public MouseSerializer(MouseInput[] inputs) => this._Inputs = inputs;
        public MouseSerializer(MouseInput input) => this._Inputs = new MouseInput[] { input };
        public MouseSerializer(byte[] bytes) => this._Bytes = bytes;

        public byte[] Serialize()
        {
            if (_Inputs.Length == 0) throw new NotSupportedException("Cannot serialize nothing!");
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(ms);
                writer.Write((int)_Inputs.Length);
                foreach (MouseInput input in _Inputs)
                {
                    writer.Write((int)input.Button);
                    writer.Write(input.X);
                    writer.Write(input.Y);
                }
                writer.Close();
                return ms.ToArray();
            }
        }
        public MouseInput[] Deserialize()
        {
            if (_Bytes.Length == 0) throw new NotSupportedException("Cannot deserialize nothing!");
            try
            {
                List<MouseInput> inputs = new List<MouseInput>();
                BinaryReader reader = new BinaryReader(new MemoryStream(_Bytes));
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int length = reader.ReadInt32();
                for (int i = 0; i < length; i++)
                {
                    MouseInput input = default(MouseInput);
                    input.Button = (Button)reader.ReadInt32();
                    input.X = reader.ReadInt32();
                    input.Y = reader.ReadInt32();
                    inputs.Add(input);
                }
                reader.Close();
                return inputs.ToArray();
            }
            catch
            {
                throw new NotSupportedException("Deserializing failed! Check your byte[]!");
            }
        }

    }
}
