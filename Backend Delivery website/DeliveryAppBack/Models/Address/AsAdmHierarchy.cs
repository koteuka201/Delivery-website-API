using System;
using System.Collections.Generic;

namespace DeliveryAppBack.Models.Address;

public partial class AsAdmHierarchy
{
   
    /// Уникальный идентификатор записи. Ключевое поле
    
    public long Id { get; set; }

   
    /// Глобальный уникальный идентификатор объекта
    
    public long? Objectid { get; set; }

   
    /// Идентификатор родительского объекта
    
    public long? Parentobjid { get; set; }

   
    /// ID изменившей транзакции
    
    public long? Changeid { get; set; }

   
    /// Код региона
    
    public string? Regioncode { get; set; }

   
    /// Код района
    
    public string? Areacode { get; set; }

   
    /// Код города
    
    public string? Citycode { get; set; }

   
    /// Код населенного пункта
    
    public string? Placecode { get; set; }

   
    /// Код ЭПС
    
    public string? Plancode { get; set; }

   
    /// Код улицы
    
    public string? Streetcode { get; set; }

   
    /// Идентификатор записи связывания с предыдущей исторической записью
    
    public long? Previd { get; set; }

   
    /// Идентификатор записи связывания с последующей исторической записью
    
    public long? Nextid { get; set; }

   
    /// Дата внесения (обновления) записи
    
    public DateOnly? Updatedate { get; set; }

   
    /// Начало действия записи
    
    public DateOnly? Startdate { get; set; }

    /// Окончание действия записи
    public DateOnly? Enddate { get; set; }

    /// Признак действующего адресного объекта
    public int? Isactive { get; set; }

    /// Материализованный путь к объекту (полная иерархия)
    public string? Path { get; set; }
}
