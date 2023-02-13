﻿using System.Runtime.Serialization;

namespace StoryHubAPI.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException() 
        {
        }

        public AuthException(string? message) : base(message)
        {
        }

        public AuthException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}