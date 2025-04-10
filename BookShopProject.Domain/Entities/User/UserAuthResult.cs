using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserAuthResult
{
    public bool Status { get; set; }

    public string StatusKey { get; set; }

    public string StatusMsg { get; set; }
}