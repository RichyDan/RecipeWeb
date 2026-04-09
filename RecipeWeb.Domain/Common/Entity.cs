namespace RecipeWeb.Domain.Common
{
    public abstract class Entity
    {
        // Список для хранения ошибок валидации
        private readonly List<ValidationError> _errors = [];
        public Guid Id { get; protected set; }

        // Свойство для получения списка ошибок
        public IReadOnlyCollection<ValidationError> Errors => _errors.AsReadOnly();

        // Проверка, есть ли ошибки
        public bool IsValid => !_errors.Any();

        // Метод для добавления ошибки
        protected void AddError(string propertyName, string errorMessage) =>
            _errors.Add(new ValidationError(propertyName, errorMessage));

        // Метод для вывода ошибок в едином виде
        public void EnsureValid()
        {
            if (!IsValid)
            {
                var errorMessages = _errors.Select(e => $"[{e.Property}]: {e.Message}");
                var summary = string.Join("; ", errorMessages);

                // Очищаем ошибки после выброса исключения
                _errors.Clear();

                throw new InvalidOperationException($"Валидация сущности {GetType().Name} не пройдена: {summary}");
            }
        }

        protected void ClearErrors() => _errors.Clear();
    }

    public record ValidationError(string Property, string Message);
}