﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileGatherer;

public class FileData
{
    public FileData()
    {
        CreatedDate = DateTime.Now;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public bool Deleted { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedDate { get; set; }
}