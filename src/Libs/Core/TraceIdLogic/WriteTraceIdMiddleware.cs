using Core.TraceLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.TraceIdLogic;

public class WriteTraceIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IEnumerable<ITraceWriter> traceWriterList)
    {
        foreach (var traceWriter in traceWriterList)
        {
            traceWriter.GetValue();
        }

        await _next(context);
    }
}