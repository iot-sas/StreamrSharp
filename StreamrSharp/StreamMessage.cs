using System;
using System.Text;
using Newtonsoft.Json.Linq;

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


           //[31, [...msgIdFields], [...msgRefFields], 27, 0, "contentData", 1, "0x29c057786Fa..."]

        public uint Version { get; set; } = 31;
        
        //MessageID   [streamId, streamPartition, timestamp, sequenceNumber, publisherId, msgChainId]
        //["stream-id", 0, 425354887214, 0, "0xAd23Ba54d26D3f0Ac057...", "msg-chain-id"]
        public string          StreamID        { get; set; } //messageid
        public UInt64          StreamPartition { get; set; } //streamPartition  number

        //MessageRef [timestamp, sequenceNumber]
        public Int64           TimeStamp        { get; set; }  //timestamp milliseconds
        public UInt32          SequenceNo       { get; set; } //sequenceNumber Sequence number of the StreamMessage within the same timestamp. Defaults to 0.
        
        public string          PublisherId      { get; set; } //publisherId  string  Id of the publisher of the StreamMessage. Must be an Ethereum address if the StreamMessage has an Ethereum signature (signatureType = 1).
        public string          MsgChainId       { get; set; } //msgChainId Id of the message chain this StreamMessage is part of. This message chain id is chosen by the publisher and defined locally for the streamId-streamPartition-publisherId triplet.
        
        
        //MessageRef [timestamp, sequenceNumber]
        public Int64           PrevTimeStamp       { get; set; } //timestamp milliseconds
        public UInt64          PrevSequenceNo      { get; set; } //sequenceNumber Sequence number of the StreamMessage within the same timestamp. Defaults to 0.
        
        
        public Content_Type    ContentType     { get; set; }
        public string          EncryptionType  { get; set; } = "0";
        public string          Content         { get; set; }
        public uint            SignatureType   { get; set; }
        public string          Signature       { get; set; }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(Version);
            sb.Append(",[\"");
            sb.Append(StreamID);
            sb.Append("\",");
            sb.Append(StreamPartition);
            sb.Append(",");
            sb.Append(TimeStamp);
            sb.Append(",");
            sb.Append(SequenceNo);
          
            sb.Append(",\"");
            sb.Append(PublisherId);
          
            sb.Append("\",\"");
            sb.Append(MsgChainId);
            sb.Append("\"],[");
            
            sb.Append(PrevTimeStamp);
            sb.Append(",");
            sb.Append(PrevSequenceNo);            
            sb.Append("],");
            
            sb.Append((uint)ContentType);            
            sb.Append(",");
            sb.Append(EncryptionType);
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

        public StreamMessage(string streamID, Content_Type contentType, string content)
        {
            ContentType = contentType;
            Content = content;
            StreamID = streamID;
        }
        
        public StreamMessage(string streamID, JObject json)
        {
            ContentType = Content_Type.Json;
            Content = json.ToString(Newtonsoft.Json.Formatting.None, new StreamrDateTimeConverter()).Replace("\"", "\\\"");
            StreamID = streamID;
            TimeStamp = DateTime.UtcNow.ToEpochMilliseconds();
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
            StreamPartition = UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            TimeStamp = Int64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
         
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            SequenceNo = UInt32.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            PublisherId = streamDataIn.Substring(pt1, pt2 - pt1);
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            MsgChainId = streamDataIn.Substring(pt1, pt2 - pt1);
            
            pt1 = pt2 + 1;
            pt2 = streamDataIn.IndexOf(',',pt1);
            ContentType = (Content_Type)UInt64.Parse(streamDataIn.Substring(pt1, pt2 - pt1));
            
            pt1 = pt2 + 1;
            pt1 = streamDataIn.IndexOf('"',pt1);
            if (pt1 == -1) return;
            pt2 = streamDataIn.LastIndexOf('}',streamDataIn.Length-1);
            Content = streamDataIn.Substring(pt1 + 1, pt2 - pt1).Replace("\\\"","\"");
            
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
