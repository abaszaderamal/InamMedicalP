namespace Med.Shared.Dtos.Todo;

public class ToDoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}