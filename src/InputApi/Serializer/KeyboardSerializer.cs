using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputApi.Keyboard;

namespace InputApi.Serializer
{
    public class KeyboardSerializer
    {
        private KeyboardInput[] _Inputs;
        private byte[] _Bytes;

        public KeyboardSerializer(KeyboardInput[] Inputs) => this._Inputs = Inputs;
        public KeyboardSerializer(KeyboardInput Input) => this._Inputs = new KeyboardInput[] { Input };
        public KeyboardSerializer(byte[] Bytes) => this._Bytes = Bytes;


        public byte[] Serialize()
        {
            if (_Inputs.Length == 0) throw new NotSupportedException("Cannot serialize nothing!");
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(ms);
                writer.Write((int)_Inputs.Length);
                foreach(KeyboardInput input in _Inputs)
                {
                    writer.Write((int)input.Keys.Length);
                    writer.Write((int)input.keyboardMethod);
                    foreach (var k in input.Keys)
                        writer.Write((int)k);
                }
                writer.Close();
                return ms.ToArray();
            }
        }

        public KeyboardInput[] Deserialize()
        {
            if (_Bytes.Length == 0) throw new NotSupportedException("Cannot deserialize nothing!");
            try
            {
                List<KeyboardInput> inputs = new List<KeyboardInput>();
                BinaryReader reader = new BinaryReader(new MemoryStream(_Bytes));
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int length = reader.ReadInt32();
                for(int i = 0; i < length; i++)
                {
                    KeyboardInput input = default(KeyboardInput);
                    int keylength = reader.ReadInt32();
                    KeyboardMethod method = (KeyboardMethod)reader.ReadInt32();
                    input.keyboardMethod = method;
                    List<Keys> keys = new List<Keys>();
                    for (int j = 0; j < keylength; j++)
                        keys.Add((Keys)reader.ReadInt32());
                    input.Keys = keys.ToArray();
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
