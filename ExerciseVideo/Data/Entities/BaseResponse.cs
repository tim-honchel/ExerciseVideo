namespace ExerciseVideo.Data.Entities
{
    [Serializable]
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
