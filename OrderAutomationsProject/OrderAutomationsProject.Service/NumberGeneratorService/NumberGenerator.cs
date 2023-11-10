using OrderAutomationsProject.Base.Model;

namespace OrderAutomationsProject.Service.NumberGeneratorService;

public interface INumberGenerator<TEntity> where TEntity : BaseModel
{
    string GenerateNumber(List<TEntity> existingBills);
}

public class NumberGenerator<TEntity>
{
    private int startNumber;
    private int endNumber;
    private string entityName;

    public NumberGenerator(int startNumber, int endNumber, string entityName)
    {
        this.startNumber = startNumber;
        this.endNumber = endNumber;
        this.entityName = entityName;
    }

    public int GenerateNumber(IEnumerable<TEntity> entities)
    {

        if (entities == null || !entities.Any())
        {
            return startNumber;
        }
        else
        {
            int lastNumber = entities.Max(GetNumberFromEntity);
            if (lastNumber < endNumber)
            {
                int nextNumber = lastNumber + 1;
                return nextNumber;
            }
            else
            {
                throw new InvalidOperationException("End number limit reached.");
            }
        }
    }

    private int GetNumberFromEntity(TEntity entity)
    {
        // Entity sınıfının int türünde Number özelliğini (property) çekiyoruz.
        var property = entity.GetType().GetProperty(entityName + "Number");
        return (int)property.GetValue(entity);
    }
}

