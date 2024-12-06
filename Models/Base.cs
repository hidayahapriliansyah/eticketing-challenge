using System.ComponentModel.DataAnnotations.Schema;

namespace eticketing.Models;

public class Base
{
    [Column(name: "created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column(name: "updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column(name: "deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // Tidak perlu pakai ini masih bisa handle case deleted data (is_deleted = deleted_at != null)
    [Column(name: "is_deleted")]
    public bool IsDeleted { get; set; }
}
