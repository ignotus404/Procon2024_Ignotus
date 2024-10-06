How to 競技鯖との通信
=====================

Author: ms0503

# 最初にやるべきこと
ビルド後成果物が入っている場所(`2024ProconTemporary/bin/Debug/net8.0/`など)に`.env`ファイルを作り、
```env
PROCON_TOKEN=<トークン>
SERVER_IP=<競技鯖のIP>
SERVER_PORT=<競技鯖のポート>
```
を書いておきます。  
(もしくはプロジェクト内(`2024ProconTemporary/`など)に`.env`を作成しプロパティで出力ディレクトリにコピーさせるようにして上記内容を記述する)

次に通信に使用するクライアントを作成しておきます。
```cs
using _2024ProconTemporary.Com;

var client = Networking.CreateClient();
```

# 問題データの取得
問題データは元のJSON形式に則った形でアクセスできるようになっています。(`_2024ProconTemporary.Com.ProblemData`を参照)

作成したクライアントを渡すと問題データが返ってきます。  
sync版・async版両方あるのでお好みでどうぞ。
```cs
// sync版
var problem = Networking.GetProblemData(client);

// async版
var problem = await Networking.GetProblemDataAsync(client);
```

# 回答データの作成
回答データは元のJSON形式に則った形で作成できるようになっています。(`_2024ProconTemporary.Com.AnswerData`を参照)
```cs
var answer = Answer.Create();
```

# 回答データの送信
作成したクライアントと回答データを渡すと送信できます。  
sync版・async版両方あるのでお好みでどうぞ。
```cs
// sync版
Networking.SendAnswerData(client, answer);

// async版
await Networking.SendAnswerDataAsync(client, answer);
```
