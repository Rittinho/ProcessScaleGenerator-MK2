namespace ProcessScaleGenerator.Shared.Constants
{
    public static class WarningTokens
    {
        public static Tuple<string, string> ExistingProcess = new(
        "Já existe esse processo!",
        "Verifique os processos existentes!"
        );
        public static Tuple<string, string> EmptyFild = new(
        "Camplo não preenchido",
        "Por favor, preencha o campo corretamente"
        );
        public static Tuple<string, string> UpdateProcess = new(
            "Alterar processo?",
            "Essa ação não tem volta!"
        );
        public static Tuple<string, string> DeleteProcess = new(
            "Excluir processo?",
            "Essa ação não tem volta!"
        );

        public static Tuple<string, string> ExistingEmployee = new(
        "Colaborador já cadastrado!",
        "Verifique os colaboradores existentes!"
        );
        public static Tuple<string, string> UpdateEmployee = new(
            "Alterar informações do colaborador?",
            "Essa ação não tem volta!"
        );
        public static Tuple<string, string> UpdatedEmployee = new(
            "Colaborador Alterado!",
            "Informações do colaborador alteradas com sucesso!"
        );
        public static Tuple<string, string> UpdatedProcess = new(
            "Processo Alterado!",
            "Informações do processo alteradas com sucesso!"
        );
        public static Tuple<string, string> DeleteEmployee = new(
            "Excluir colaborador?",
            "Essa ação não tem volta!"
        );

        public static Tuple<string, string> CorruptFile = new(
            "Arquivo inacessivel!",
            "Arquivo corrompido, inacessivel ou inexistente!"
        );
        public static Tuple<string, string> DescarteUpdate = new(
            "Descartar alterações?",
            "Essa ação não tem volta!"
        );
        public static Tuple<string, string> CreateSuccess = new(
            "Criado com sucesso!",
            "Criado com sucesso, já pode ser visto na lista"
        );
        public static Tuple<string, string> DeleteSuccess = new(
            "Deletado com sucesso!",
            "Deletado com sucesso, já pode ser visto na lista"
        );
        public static Tuple<string, string> UpdateSuccess = new(
            "Alterado com sucesso!",
            "Alterado com sucesso, já pode ser visto na lista"
        );
    }
}
