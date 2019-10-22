using System;
using System.Text;

namespace StreamrSharp
{
    public class StreamMessage
    {

        public enum Content_Type
        {
            Json = 27,
            GroupKeyRequest = 28,
            GroupKeyResponse = 29,
            GroupKeyReset = 30
        }

        public enum Signature_Type
        {
            None = 0,
            ETH_Legacy = 1,
            ETH = 2
        }
    
        public uint            Version         { get; set; }
        public string          StreamID        { get; set; }
        public UInt64          DONTKNOW1       { get; set; }
        public UInt64          MessageID       { get; set; }
        public UInt64          DONTKNOW2       { get; set; }
        public UInt64          MessageID2      { get; set; }
        public UInt64          MessageIDPrev   { get; set; }
        public Content_Type    ContentType     { get; set; }
        public string          Content         { get; set; }
        public uint            SignatureType   { get; set; }
        public string          Signature       { get; set; }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(Version);
            sb.Append(",\"");
            sb.Append(StreamID);
            sb.Append("\",");
            sb.Append(DONTKNOW1);
            sb.Append(",");
            sb.Append(MessageID);
            sb.Append(",");
            sb.Append(DONTKNOW2);
            sb.Append(",");
            sb.Append(MessageID2);
            sb.Append(",");
            sb.Append(MessageIDPrev);
            sb.Append(",\"");
            sb.Append(Content);
            sb.Append("\"");

            if (SignatureType == (uint)Signature_Type.None)
            {
                sb.Append(",");
                sb.Append(SignatureType);
                sb.Append(",\"");
                sb.Append(Signature);
                sb.Append("\"");
            }
            
            sb.Append("]");

            return sb.ToString();
        }



        public StreamMessage(Content_Type contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }
        
        public StreamMessage(string streamDataIn)
        {
            int pt1 = 1;
            int pt2 = streamDataIn.IndexOf(',');
            Version = uint.Parse(streamDataIn.Substring(pt1, pt2 - pt1));

            pt1 = pt2 + 2;
            pt2 = streamDataIn.IndexOf('"',pt1);
            StreamID = streamDataIn.Substring(pt1, pt2 - pt1);
            
            pt1 = pt2 + 2;
            pt2 = streamDataIn.IndexOf(',',pt1);
            DONTKNOW1 = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            MessageID = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
         
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            DONTKNOW2 = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            MessageID2 = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            MessageIDPrev = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            ContentType = (Content_Type)UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt1 = streamDataIn.IndexOf('"',pt1);
            if (pt1 == -1) return;
            pt2 = streamDataIn.LastIndexOf('}',streamDataIn.Length-1);
            Content = streamDataIn.Substring(pt1 + 1, pt2 - pt1);
            
            pt1 = pt2 + 2;
            if (pt1 >= streamDataIn.Length) return;
            pt2 = streamDataIn.IndexOf(',',pt1);
            if (pt2 == -1) return;
            SignatureType = uint.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 2;
            if (pt1 >= streamDataIn.Length) return;
            pt2 = streamDataIn.IndexOf('"',pt1);
            if (pt2 == -1) return;
            Signature = streamDataIn.Substring(pt1, pt2 - pt1);

        }
    }
}
