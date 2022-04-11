using System.Text;

namespace IcuTech.Web.Serialization;

public class EncodingStringWriter : StringWriter
{
    public EncodingStringWriter(Encoding encoding) => this.Encoding = encoding;

    public override Encoding Encoding { get; }
}