using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Common
{
    public class ResponseDto
    {
        internal ResponseDto(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        internal ResponseDto(bool succeeded, string successMessage, int id = 0, string data = null)
        {
            Succeeded = succeeded;
            SuccessMessage = successMessage;
            Id = id;
            Data = data;
        }

        public int Id { get; set; }
        public bool Succeeded { get; set; }
        public string SuccessMessage { get; set; }
        public string Data { get; set; }
        public string[] Errors { get; set; }

        public static ResponseDto Success(string messeage, int id = 0, string data = null)
        {
            return new ResponseDto(true, messeage, id, data);
        }

        public static ResponseDto Failure(IEnumerable<string> errors)
        {
            return new ResponseDto(false, errors);
        }
    }
}
