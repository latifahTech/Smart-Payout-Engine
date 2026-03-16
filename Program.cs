var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => Results.Content(@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Fintech Payout Engine</title>
        <link rel='preconnect' href='https://fonts.googleapis.com'>
        <link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>
        <link href='https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap' rel='stylesheet'>
        <style>
            body { font-family: 'Inter', sans-serif; background-color: #f8fafc; display: flex; justify-content: center; align-items: center; min-height: 100vh; margin: 0; }
            .card { background: white; padding: 40px; border-radius: 20px; box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1); width: 100%; max-width: 400px; border: 1px solid #e2e8f0; }
            .logo { font-weight: 600; font-size: 22px; color: #0f172a; margin-bottom: 24px; text-align: center; }
            .form-group { margin-bottom: 16px; text-align: left; }
            label { display: block; font-size: 13px; font-weight: 500; color: #64748b; margin-bottom: 6px; }
            input { width: 100%; padding: 12px; border: 1px solid #e2e8f0; border-radius: 8px; box-sizing: border-box; font-family: inherit; font-size: 14px; transition: border 0.2s; }
            input:focus { outline: none; border-color: #2563eb; ring: 2px solid #bfdbfe; }
            .btn { background-color: #2563eb; color: white; width: 100%; padding: 14px; border: none; border-radius: 10px; font-weight: 600; font-size: 14px; cursor: pointer; transition: all 0.2s; margin-top: 10px; }
            .btn:hover { background-color: #1d4ed8; transform: translateY(-1px); }
            .footer { margin-top: 24px; text-align: center; font-size: 11px; color: #94a3b8; }
        </style>
    </head>
    <body>
        <div class='card'>
            <div class='logo'>Smart Repayment Schedule Generator</div>
            <form action='/api/Payout/calculate' method='get'>
                <div class='form-group'>
                    <label>Principal Amount ($)</label>
                    <input type='number' name='amount' placeholder='e.g. 10000' required min='1'>
                </div>
                <div class='form-group'>
                    <label>Annual Interest Rate (%)</label>
                    <input type='number' step='0.01' name='annualRate' placeholder='e.g. 12' required min='0.1'>
                </div>
                <div class='form-group'>
                    <label>Duration (Months)</label>
                    <input type='number' name='months' placeholder='e.g. 6' required min='1'>
                </div>
                <button type='submit' class='btn'>Generate Schedule</button>
            </form>
            <div class='footer'>Secure Financial Calculation API &bull; 2026</div>
        </div>
    </body>
    </html>", "text/html"));

app.UseHttpsRedirection();
app.MapControllers();
app.Run();