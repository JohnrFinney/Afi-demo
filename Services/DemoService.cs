using afi_demo.Classes;
using afi_demo.Models.Requests;
using afi_demo.Services.Interfaces;
using afi_demo.Services.Repositories.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace afi_demo.Services;

public class DemoService : IDemoService
{
    private readonly IConfiguration _configuration;
    private readonly IDemoRepository _demoRepository;

    public DemoService(IConfiguration configuration,
                       IDemoRepository demoRepository)
    {
        this._configuration = configuration;
        _demoRepository = demoRepository;
    }


    public async Task<int?> RegisterNewUser(RegisterUserRequest model, bool validateAll = false)
    {
        var modelValidationResults = await this.IsValid(model, validateAll);
        if (modelValidationResults.IsValid == false)
        {
            if (validateAll == false)
            {
                throw new CustomException(modelValidationResults.ValidationErrors.First().Value, 400);
            }
            else
                throw new CustomException("Validation error/s found. Please correct and resubmit.", modelValidationResults.ValidationErrors, 400);
        }

        var res = await this._demoRepository.RegisterNewUser(model);

        return res;
    }






    private async Task<(bool IsValid, IDictionary<string, string> ValidationErrors)> IsValid(RegisterUserRequest model, bool validateAll)
    {
        var isValid = true;
        IDictionary<string, string> validationErrors = new Dictionary<string, string>();

        // First name
        if (String.IsNullOrEmpty(model.FirstName.Trim()))
        {
            validationErrors.Add("NoFirstName", "First name is required.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // Surname
        if (String.IsNullOrEmpty(model.Surname.Trim()))
        {
            validationErrors.Add("NoSurname", "Surname is required.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // Check for at least date of birth or email details
        if (String.IsNullOrEmpty(model.Email) && !model.DateOfBirth.HasValue)
        {
            validationErrors.Add("NoEmailOrDateOfBirth", "Either email or date of birth is required. Please enter details for at least one of these and resubmit.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // First name
        if (model.FirstName.Trim().Length < 4 || model.FirstName.Trim().Length > 49)
        {
            validationErrors.Add("FirstNameLength", "First name length must be between 3 and 50 long.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // Surname
        if (model.Surname.Trim().Length < 4 || model.Surname.Trim().Length > 49)
        {
            validationErrors.Add("SurnameLength", "Surname length must be between 3 and 50 long.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // Policy ref
        var policyRegex = new Regex(@"[A-Z]{2}-\d{6}");
        var policyMatch = policyRegex.IsMatch(model.ReferenceNumber);
        if (!policyMatch)
        {
            validationErrors.Add("PolicyMatch", $"Policy reference pattern does not match.");
            if (validateAll == false)
                return (false, validationErrors);

            isValid = false;
        }

        // Date of birth
        if (model.DateOfBirth.HasValue)
        {
            if (model.DateOfBirth.Value > DateTime.Now)
            {
                validationErrors.Add("FutureAge", $"Applicant's date of birth '{model.DateOfBirth.Value.ToString("yyyy-MM-dd")}' is after today.");
                if (validateAll == false)
                    return (false, validationErrors);

                isValid = false;
            }

            var minimalAge = System.Convert.ToInt32(_configuration["App:MinimumPolicyAge"]);
            if (model.DateOfBirth.Value > DateTime.Now.AddYears(minimalAge * -1))
            {
                validationErrors.Add("MinimumPolicyAge", $"Applicant's age of '{model.DateOfBirth.Value.ToString("yyyy-MM-dd")}' is below the current minimum of {minimalAge}.");
                if (validateAll == false)
                    return (false, validationErrors);

                isValid = false;
            }
        }

        // Email
        if (!String.IsNullOrEmpty(model.Email))
        {
            var acceptableEmail = false;
            if (!model.Email.Trim().ToLower().EndsWith(".com") && !model.Email.Trim().ToLower().EndsWith(".co.uk"))
            {
                validationErrors.Add("EmailSource", $"Applicant's email address from unacceptable region/range.");
                if (validateAll == false)
                    return (false, validationErrors);

                isValid = false;
            }

            var emailRegex = new Regex(@"[aA-zZ]{4}@[aA-zZ]{2}");
            var emailMatch = emailRegex.IsMatch(model.ReferenceNumber);
            if (!policyMatch)
            {
                validationErrors.Add("EmailMatch", $"Email pattern does not match current business rules for email addresses.");
                if (validateAll == false)
                    return (false, validationErrors);

                isValid = false;
            }

        }

        return (isValid, validationErrors);
    }



}

