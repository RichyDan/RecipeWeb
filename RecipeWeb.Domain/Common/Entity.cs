namespace RecipeWeb.Domain.Common;

public abstract class Entity
{
    // Список для хранения ошибок валидации
    private readonly List<ValidationError> errors =[];

    public Guid Id { get; protected set; }

    // Свойство для получения списка ошибок
    public IReadOnlyCollection<ValidationError> Errors => this.errors.AsReadOnly();

    // Проверка, есть ли ошибки
    public bool IsValid => !this.errors.Any();

    // Метод для добавления ошибки
    protected void AddError(string propertyName, string errorMessage) =>
        this.errors.Add(new ValidationError(propertyName, errorMessage));

    // Метод для вывода ошибок в едином виде
    public void EnsureValid()
    {
        if (!this.IsValid)
        {
            var errorMessages = this.errors.Select(e => $"[{e.property}]: {e.message}");
            var summary = string.Join("; ", errorMessages);

            // Очищаем ошибки после выброса исключения
            this.errors.Clear();

            throw new InvalidOperationException($"Валидация сущности {this.GetType().Name} не пройдена: {summary}");
        }
    }

    protected void ClearErrors() => this.errors.Clear();
}

public record ValidationError(string property, string message);