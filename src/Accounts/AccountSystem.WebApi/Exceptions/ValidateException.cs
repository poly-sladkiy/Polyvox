namespace AccountSystem.WebApi.Exceptions;

public class ValidateException(string details, params string[] FieldNames) : Exception(details);
