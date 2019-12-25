using System.Collections.Generic;

namespace TBP.Services.Result
{
    public class ServiceResult
    {
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; } = new List<string>();

        public void CleanErrors()
        {
            Success = true;
            Errors.Clear();
        }

        public void SetErrorMessage(string message)
        {
            if (Success)
                Success = false;

            Errors.Add(message);
        }

        public void SetErrorMessages(List<string> messages)
        {
            foreach (var message in messages)
                SetErrorMessage(message);
        }
    }
}
