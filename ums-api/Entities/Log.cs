﻿namespace ums_api.Entities
{
    public class Log: BaseEntity<long>
    {
        public string? UserName { get; set; }
        public string Description { get; set; }
    }
}
