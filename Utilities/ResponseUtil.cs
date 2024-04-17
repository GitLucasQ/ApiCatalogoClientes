using ApiCatalogoClientes.Data.Response;

namespace ApiCatalogoClientes.Utilities
{
    public class ResponseUtil
    {
        public static GeneralResponse Ok(string? message = "", object? data = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return new GeneralResponse { Status = StatusCodes.Status200OK, Message = Variables.MessageResponse.Success, Data = data };
            }

            return new GeneralResponse { Status = StatusCodes.Status200OK, Message = message, Data = data };
        }

        public static GeneralResponse Error(string? message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                return new GeneralResponse { Status = StatusCodes.Status400BadRequest, Message = Variables.MessageResponse.Error, Data = null };
            }

            return new GeneralResponse { Status = StatusCodes.Status400BadRequest, Message = message, Data = null };
        }

        public static GeneralResponse Create(string? message = "", object? data = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return new GeneralResponse { Status = StatusCodes.Status201Created, Message = Variables.MessageResponse.Success, Data = data };
            }

            return new GeneralResponse { Status = StatusCodes.Status201Created, Message = message, Data = data };
        }

        public static GeneralResponse NotFound(string? message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                return new GeneralResponse { Status = StatusCodes.Status404NotFound, Message = Variables.MessageResponse.NotFound, Data = null };
            }

            return new GeneralResponse { Status = StatusCodes.Status404NotFound, Message = message, Data = null };
        }
    }
}
