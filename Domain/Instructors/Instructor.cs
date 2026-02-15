using Domain.Common.Exceptions;

namespace Domain.Instructors;

public sealed class Instructor
{
    public Instructor(string? id, string? firstName, string? lastName, string? email, string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new DomainValidationException("Id is required.");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("FirstName is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("LastName is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required.");

        Id = id.Trim();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber.Trim();
    }

    public string Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}