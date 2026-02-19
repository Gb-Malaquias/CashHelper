decimal salario = 0;
string option;

var calculador = new CalculadorGeral();


 Console.WriteLine("Digite seu salário mensal:");
            salario = decimal.Parse(Console.ReadLine()!);
                if (salario <= 0)
                {
                    Console.WriteLine("Salário inválido.");
                    return;
                }
Console.WriteLine();

        




do {



Console.WriteLine("Gerenciador de Gastos Pessoais");
Console.WriteLine("------------------------");
Console.WriteLine("Opções:");
Console.WriteLine("1. Adicionar novo gasto");
Console.WriteLine("2. remover um gasto");
Console.WriteLine("3. Listar todas os gastos");
Console.WriteLine("4. Listar gastos do mês");
Console.WriteLine("5. Calcular dinheiro restante");
Console.WriteLine("6- Análize salarial");
Console.WriteLine("0. Sair");
Console.WriteLine("------------------------");
option = Console.ReadLine()!;
Console.WriteLine();

    switch (option)
    {
        case "1":
    try
    {
        Console.WriteLine();
        Console.WriteLine("Escolha a categoria do gasto:");
        Console.WriteLine("1. Alimentação");
        Console.WriteLine("2. Moradia");
        Console.WriteLine("3. Transporte");
        Console.WriteLine("4. Lazer");
        Console.WriteLine("5. Saúde");
        Console.WriteLine("6. Educação");
        Console.WriteLine("7. Outros");

        if (!int.TryParse(Console.ReadLine(), out int catEscolhida) ||
            !Enum.IsDefined(typeof(TipoGasto), catEscolhida))

        {
            Console.WriteLine("Categoria inválida.");
            break;
        }

        TipoGasto categoria = (TipoGasto)catEscolhida;

        string? descricao = null;
        if (categoria == TipoGasto.Outros)
        {
            Console.WriteLine("Digite a descrição do gasto:");
            descricao = Console.ReadLine();
        }
        

        Console.WriteLine("Digite o valor gasto:");

            if (!decimal.TryParse(Console.ReadLine(), out decimal valorGastado) || valorGastado <= 0)
                {
                    Console.WriteLine("Valor inválido.");
                    break;
                }

        var gasto = calculador.Adicionar(categoria, descricao, valorGastado);

        Console.WriteLine(
            $"Gasto adicionado: ID {gasto.Id}, Categoria {gasto.Categoria}, Valor {gasto.Valor:C}"
        );
        Console.WriteLine();

     }
     catch (Exception ex)
     {
            Console.WriteLine($"Erro ao adicionar gasto: {ex.Message}");
            Console.WriteLine();
     }
        break;


        case "2":
            Console.WriteLine("Digite o ID do gasto que deseja remover:");

            if (!int.TryParse(Console.ReadLine(), out int idRemover) || idRemover <= 0)
            {
                Console.WriteLine("ID inválido.");
                break;
            }
            try
            {
                var gastoRemovido = calculador.Remover(idRemover);
                Console.WriteLine(
                    $"Removido: ID {gastoRemovido.Id}, " +
                    $"Categoria {gastoRemovido.Categoria}, " +
                    $"Valor {gastoRemovido.Valor:C}"
                    
                );
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;



        case "3":
            var gastos = calculador.Listar();
            if (gastos.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Nenhum gasto registrado.");
                Console.WriteLine();
            }
            else
            {
                foreach (var g in gastos)
                {
                    Console.WriteLine($"ID: {g.Id}, Categoria: {g.Categoria}, Valor: {g.Valor:C}, Data: {g.Data}");
                    if (!string.IsNullOrEmpty(g.Descricao))
                    {
                        Console.WriteLine($"   Descrição: {g.Descricao}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine($"Total gasto: {calculador.Total():C}");
                Console.WriteLine();
            }
            break;



        case "4":
            Console.WriteLine("Digite o número do mês para filtrar os gastos (1-12):");

            if (!int.TryParse(Console.ReadLine(), out int mes) || mes < 1 || mes > 12)
            {
                Console.WriteLine("Mês inválido.");
                break;
            }

            var gastosMes = calculador.ListarPorMes(mes);

            if (gastosMes.Count == 0)
            {
                Console.WriteLine($"Nenhum gasto registrado no mês {mes}.");
                break;
            }

            decimal totalMes = 0;

            foreach (var g in gastosMes)
            {
                Console.WriteLine(
                    $"ID: {g.Id}, Categoria: {g.Categoria}, Valor: {g.Valor:C}, Data: {g.Data:d}"
                );

                if (!string.IsNullOrWhiteSpace(g.Descricao))
                {
                    Console.WriteLine($"   Descrição: {g.Descricao}");
                }

                totalMes += g.Valor;
            }
            Console.WriteLine();
            Console.WriteLine($"Total gasto no mês {mes}: {totalMes:C}");
            Console.WriteLine();
            break;



        case "5":
           
            decimal restante = salario - calculador.Total();
            Console.WriteLine();
            Console.WriteLine($"Dinheiro restante após os gastos: {restante:C}");
            Console.WriteLine();
            break;




        case "6":

            Console.WriteLine("Análise de otimização salarial:");
            Console.WriteLine();

            if (calculador.Listar().Count == 0)
            {
                Console.WriteLine("Nenhum gasto registrado para análise.");
                break;
            }

            var recomendador = new Recomendador(salario, calculador.Listar());

            foreach (var mensagem in recomendador.Avaliar())
            {
                Console.WriteLine(mensagem);
            }

            Console.WriteLine();
            break;









        case "0":
            Console.WriteLine("Encerrando o programa. Até mais!");
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;


    }

} while (option != "0");



