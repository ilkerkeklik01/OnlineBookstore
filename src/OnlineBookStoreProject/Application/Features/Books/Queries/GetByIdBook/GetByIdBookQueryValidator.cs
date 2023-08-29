





using FluentValidation;

namespace Application.Features.Books.Queries.GetByIdBook
{
    public class GetByIdBookQueryValidator:AbstractValidator<GetByIdBookQuery>
    {
        public GetByIdBookQueryValidator()
        {
            RuleFor(x => x.Id).Must(ValidateId);
        }

        private bool ValidateId(int id)
        {
            if(id<1) return false;
            return true;
        }

    }


}

