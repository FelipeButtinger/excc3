namespace Inventario.Models
{
    public class InventarioRecord
    {
        public int Id { get; set; }

        public int IdPersonagem { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public List<ItemRecord> Itens { get; set; }

        public InventarioRecord()
        {
            Id = 0;
            IdPersonagem = 0;
            Descricao = "";
            Quantidade = 1;
            Itens = new List<ItemRecord>();
        }
    }

    public class ItemRecord
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string TipoId { get; set; }

        public bool IsEquipavel { get; set; }

        public bool IsConsumivel { get; set; }

        public int Dano { get; set; }

        public int Defesa { get; set; }

        public ItemRecord()
        {
            Id = 0;
            Nome = "";
            Descricao = "";
            TipoId = "";
            IsEquipavel = false;
            IsConsumivel = false;
            Dano = 0;
            Defesa = 0;
        }
    }

    public class TipoItemRecord
    {
        public string IdTipoItem { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public TipoItemRecord()
        {
            IdTipoItem = "";
            Nome = "";
            Descricao = "";
        }
    }
}