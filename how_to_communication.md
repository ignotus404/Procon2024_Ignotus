How to 競技鯖との通信
=====================

Author: ms0503

# 最初にやるべきこと
`2024ProconTemporary`内に`.env`ファイルを作り、
```env
PROCON_TOKEN=<トークン>
SERVER_IP=<競技鯖のIP>
```
を書いておきます。

次に通信に使用するクライアントを作成しておきます。
```cs
using _2024ProconTemporary.Com;

var client = Networking.CreateClient();
```

# 問題データの取得
問題データは元のJSON形式に則った形でアクセスできるようになっています。(`_2024ProconTemporary.Com.ProblemData`を参照)
```cs
// sync版
var problem = Networking.GetProblemData(client);

// async版
var problem = await Networking.GetProblemDataAsync(client);
```

# 回答データの送信
回答データは元のJSON形式に則った形で作成できるようになっています。(`_2024ProconTemporary.Com.AnswerData`を参照)
```cs
// sync版
Networking.SendAnswerData(client, answer);

// async版
await Networking.SendAnswerDataAsync(client, answer);
```
