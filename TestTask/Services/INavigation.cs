﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Services
{
    public interface INavigation
    {
        void NavigateTo(BaseViewModel viewModel);
    }
}
