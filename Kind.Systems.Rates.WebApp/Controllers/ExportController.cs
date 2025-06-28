using ClosedXML.Excel;
using Kind.Systems.Rates.WebApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kind.Systems.Rates.WebApp.Controllers
{
    [ApiController]
    [Route("api/export")]
    public class ExportController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{baseCur}")]
        public async Task<IActionResult> Get(string baseCur, CancellationToken ct = default)
        {
            var res = await mediator.Send(new GetLatestRatesQuery(baseCur), ct);
            if (!res.IsSuccess || res.Value.Count == 0)
                return NotFound("Нет данных для экспорта");

            using var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Курсы");

            // Главка документа
            ws.Cell(1, 1).Value = "Базовая валюта:";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 2).Value = baseCur.ToUpperInvariant();

            ws.Cell(2, 1).Value = "Дата и время получения:";
            ws.Cell(2, 1).Style.Font.Bold = true;
            ws.Cell(2, 2).Value = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            // заголовки таблицы
            ws.Cell(4, 1).Value = "Валюта";
            ws.Cell(4, 2).Value = "Курс";
            ws.Range("A4:B4").Style.Font.Bold = true;

            // данные курсов
            for (var i = 0; i < res.Value.Count; i++)
            {
                ws.Cell(i + 5, 1).Value = res.Value[i].Quote;
                ws.Cell(i + 5, 2).Value = res.Value[i].Rate;
            }

            // сохраним в стрим памяти и потом даем в ответ файл
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Position = 0;

            var fileName = $"курсы_{baseCur}_{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx";
            return File(ms.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
        }
    }
}
