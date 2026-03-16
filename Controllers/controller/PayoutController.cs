using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FintechPayoutEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayoutController : ControllerBase
    {
        [HttpGet("calculate")]
        public IActionResult Calculate(decimal amount, decimal annualRate, int months)
        {
            if (amount <= 0 || annualRate <= 0 || months <= 0)
            {
                return BadRequest("<h1 style='font-family:sans-serif; text-align:center; color:#e74c3c; margin-top:50px;'>Invalid Input. All values must be greater than zero.</h1>");
            }

            decimal monthlyRate = (annualRate / 100) / 12;
            decimal principalPerMonth = amount / months;
            var startDate = DateTime.Now;

            var html = new StringBuilder();
            html.Append(@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Payout Schedule | Results</title>
                    <link rel='preconnect' href='https://fonts.googleapis.com'>
                    <link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>
                    <link href='https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap' rel='stylesheet'>
                    <style>
                        body { 
                            font-family: 'Inter', sans-serif; 
                            background-color: #f8fafc; 
                            color: #1e293b;
                            margin: 0;
                            padding: 40px;
                            display: flex;
                            justify-content: center;
                        }
                        .container {
                            background: white;
                            padding: 48px;
                            border-radius: 20px;
                            box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1);
                            max-width: 800px;
                            width: 100%;
                            border: 1px solid #e2e8f0;
                        }
                        .header {
                            text-align: center;
                            margin-bottom: 40px;
                            border-bottom: 1px solid #f1f5f9;
                            padding-bottom: 20px;
                        }
                        .title {
                            font-weight: 600;
                            font-size: 28px;
                            color: #0f172a;
                            margin: 0;
                            letter-spacing: -0.025em;
                        }
                        .subtitle {
                            color: #64748b;
                            font-size: 16px;
                            margin-top: 8px;
                        }
                        .summary {
                            display: grid;
                            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
                            gap: 20px;
                            background-color: #f1f5f9;
                            padding: 24px;
                            border-radius: 12px;
                            margin-bottom: 40px;
                        }
                        .summary-item { text-align: center; }
                        .summary-label { font-size: 12px; color: #64748b; font-weight: 500; text-transform: uppercase; letter-spacing: 0.05em; }
                        .summary-value { font-size: 18px; color: #0f172a; font-weight: 600; margin-top: 4px; }

                        table {
                            width: 100%;
                            border-collapse: collapse;
                            font-size: 14px;
                        }
                        th {
                            text-align: left;
                            padding: 16px;
                            background-color: #f8fafc;
                            color: #475569;
                            font-weight: 600;
                            border-bottom: 2px solid #e2e8f0;
                        }
                        td {
                            padding: 16px;
                            border-bottom: 1px solid #f1f5f9;
                            color: #1e293b;
                        }
                        tr:last-child td { border-bottom: none; }
                        tr:hover td { background-color: #f1f5f9; }
                        .money { font-family: 'Courier New', monospace; font-weight: 600; text-align: right; }
                        
                        .back-btn {
                            display: block;
                            text-align: center;
                            margin-top: 40px;
                            text-decoration: none;
                            color: #2563eb;
                            font-weight: 500;
                            font-size: 14px;
                        }
                        .back-btn:hover { text-decoration: underline; }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1 class='title'>Payout Schedule</h1>
                            <p class='subtitle'>Detailed breakdown of interest distribution and principal repayment.</p>
                        </div>
                        
                        <div class='summary'>
                            <div class='summary-item'><div class='summary-label'>Principal</div><div class='summary-value'>$" + amount.ToString("N2") + @"</div></div>
                            <div class='summary-item'><div class='summary-label'>Annual Rate</div><div class='summary-value'>" + annualRate.ToString("N2") + @"%</div></div>
                            <div class='summary-item'><div class='summary-label'>Term</div><div class='summary-value'>" + months + @" Months</div></div>
                        </div>

                        <table>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Due Date</th>
                                    <th style='text-align:right;'>Principal</th>
                                    <th style='text-align:right;'>Interest</th>
                                    <th style='text-align:right;'>Total</th>
                                </tr>
                            </thead>
                            <tbody>");

            for (int i = 1; i <= months; i++)
            {
                decimal interest = principalPerMonth * monthlyRate;
                decimal total = principalPerMonth + interest;
                DateTime dueDate = startDate.AddMonths(i);

                html.Append("<tr>");
                html.Append("<td>" + i + "</td>");
                html.Append("<td>" + dueDate.ToString("MMM dd, yyyy") + "</td>");
                html.Append("<td class='money'>" + principalPerMonth.ToString("N2") + "</td>");
                html.Append("<td class='money'>" + interest.ToString("N2") + "</td>");
                html.Append("<td class='money'>" + total.ToString("N2") + "</td>");
                html.Append("</tr>");
            }

            html.Append(@"
                            </tbody>
                        </table>
                        <a href='/' class='back-btn'>&larr; Back to API Home</a>
                    </div>
                </body>
                </html>");

            return Content(html.ToString(), "text/html");
        }
    }
}
