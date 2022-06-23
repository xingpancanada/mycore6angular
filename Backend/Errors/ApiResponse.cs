namespace Backend.Errors
{
    ///////51. Creating a consistent error response from the API
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            //!!!Switch new express without break !!!!!!
            return statusCode switch
            {
                400 => "400: Bad request.",
                401 => "401: No authorization.",
                404 => "404: The page doesn't exist anymore.",
                500 => "500: Internal server error.",
                _ => null,
            };
        }
        
    }
    
}