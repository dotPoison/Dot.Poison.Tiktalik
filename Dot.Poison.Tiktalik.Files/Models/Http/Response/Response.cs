using System.Net;
using Dot.Poison.Tiktalik.Files.Interfaces;

namespace Dot.Poison.Tiktalik.Files.Models.Http.Response;

public class Response
{
    public HttpStatusCode StatusCode { get; set; }
}