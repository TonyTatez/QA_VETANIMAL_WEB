namespace VET_ANIMAL.WEB.Interfaces
{
    public interface IServerSideReportingEngine : IDisposable
    {
        byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList);

        byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList, out string reportFileName);

        byte[] GenerateReportToByteArray<T>(IEnumerable<T> dataList, out string reportFileName, out TimeSpan reportGenerationTime);

        MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList);

        MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList, out string reportFileName);

        MemoryStream GenerateReportToStream<T>(IEnumerable<T> dataList, out string reportFileName, out TimeSpan reportGenerationTime);
    }
}