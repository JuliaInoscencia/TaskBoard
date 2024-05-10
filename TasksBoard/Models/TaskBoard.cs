using System.ComponentModel.DataAnnotations;

namespace TasksBoard.Models;
public class TaskBoard
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; }

    [Display(Name = "Data de Início")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Display(Name = "Data de Conclusão")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CompleteDate { get; set; }

    [Display(Name = "Status")]
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.ToDo;
}

public enum TaskStatusEnum
{
    ToDo,
    InProgress,
    Done
}
