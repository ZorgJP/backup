using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Json;

namespace backup
{
    public class DirectoryConverter : JsonConverter<DirectoryInfo>
    {
        public override DirectoryInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new DirectoryInfo(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.FullName);
        }
    }
}
