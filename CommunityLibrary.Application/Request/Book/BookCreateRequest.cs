using System;
using System.ComponentModel.DataAnnotations;

public class BookCreateRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [DataType(DataType.Date, ErrorMessage = "Published date must be a valid date.")]
    [Required(ErrorMessage = "Published date is required.")]
    public DateTime? PublishedDate { get; set; } = null;

    //[Required(ErrorMessage = "Status is required.")]
    //[RegularExpression("^(Active|Inactive|Pending)$", ErrorMessage = "Status must be 'Active', 'Inactive', or 'Pending'.")]
    public bool Status { get; set; } = true;

    [Required(ErrorMessage = "AuthorId is required.")]
    public Guid AuthorId { get; set; }

    [Required(ErrorMessage = "BookCategoryId is required.")]
    public Guid BookCategoryId { get; set; }

    [Required(ErrorMessage = "RegisteredByUserId is required.")]
    public Guid RegisteredByUserId { get; set; }

}
