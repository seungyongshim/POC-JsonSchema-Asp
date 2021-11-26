namespace WebApi.Dto
{
    public record Attachment(string FileName,
                             string ContentId, 
                             byte[] Data);
}
