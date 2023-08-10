using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {


        public CreateBookCommandValidator()
        {
            RuleFor(x=>x.Author).NotNull();
            RuleFor(x=>x.Author).MinimumLength(3).NotEmpty();
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.Title).MinimumLength(1).NotEmpty();
            RuleFor(x => x.Price).LessThanOrEqualTo(100);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(1);
        }


        

    }
}
