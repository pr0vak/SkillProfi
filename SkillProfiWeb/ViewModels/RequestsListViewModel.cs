﻿using SkillProfi.DAL.Models;

namespace SkillProfiWeb.ViewModels
{
    public class RequestsListViewModel
    {
        public IEnumerable<Request> Requests { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentStatus { get; set; }
    }
}
