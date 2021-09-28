﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepositApi.Models
{
    public class DepositModel
    {
        public int Id { get; set; }

        public double? Amount { get; set; }

        public int? Term { get; set; }
        
        public double? Percent { get; set; }

        public DateTime Date { get; set; }
    }

    public class DepositValidator : AbstractValidator<DepositModel>
    {
        public DepositValidator()
        {
            RuleFor(d => d.Amount).NotNull();
            RuleFor(d => d.Term).NotNull();
            RuleFor(d => d.Percent).NotNull();
        }
    }
}
