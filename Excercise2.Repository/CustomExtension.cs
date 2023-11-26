using System;

namespace Excercise2.Repository
{
    /// <summary>
    /// Custom extension class for override all default functions
    /// </summary>
    public static class CustomExtension
    {
        /// <summary>
        /// Method to utilize the existing object and change into exception object
        /// </summary>
        /// <param name="p_response"></param>
        public static void Exception(this Response p_response, Exception p_exception = null)
        {
            p_response.IsError = true;
            if (p_exception != null)
                p_response.Message = p_exception.Message;
            else p_response.Message = Status.Exception.ToString();
            p_response.StatusCode = Convert.ToInt32(Status.Failed);
        }
    }
}
