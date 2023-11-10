﻿using System.Text.Json;

namespace OrderAutomationsProject.Base.Response;

public class ApiResponse
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public ApiResponse(string message = null)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Success = true;
        }
        else
        {
            Success = false;
            Message = message;
        }
    }

    public bool Success { get; set; }
    public string Message { get; set; }
}

public partial class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse(bool isSuccess)
    {
        Success = isSuccess;
        Response = default;
        Message = isSuccess ? "Success" : "Error";
    }

    public ApiResponse(T data)
    {
        Success = true;
        Response = data;
        Message = "Success";
    }

    public ApiResponse(string message)
    {
        Success = false;
        Response = default;
        Message = message;
    }

    public ApiResponse(int statusCode, string message)
    {
        Success = false;
        Response = default;
        Message = message;
        StatusCode = statusCode;
    }
}
