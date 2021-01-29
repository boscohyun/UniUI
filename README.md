# UniUI

[![openupm](https://img.shields.io/npm/v/com.boscohyun.uniui?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.boscohyun.uniui/)
[![openupm](https://img.shields.io/npm/v/com.boscohyun.uniui.examples?label=openupm(examples)&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.boscohyun.uniui.examples/)

- **UniUI** pursues UI management with the simplest structure.
- **UniUI** uses [UniRx](https://github.com/neuecc/UniRx) and [UniTask](https://github.com/Cysharp/UniTask) to obtain various advantages.

# UPM

You can edit `manifest.json` to add **UniUI** and example packages.

```json
{
  "dependencies": {
    "com.boscohyun.uniui": "https://github.com/boscohyun/UniUI.git?path=Assets/Plugins/UniUI/Scripts#main",
    "com.boscohyun.uniui.examples": "https://github.com/boscohyun/UniUI.git?path=Assets/Plugins/UniUI/Examples#main",
  }
}
```

If you want a specific release version(e.g.,`0.1.0`), you can add it like below.

```json
{
  "dependencies": {
    "com.boscohyun.uniui": "https://github.com/boscohyun/UniUI.git?path=Assets/Plugins/UniUI/Scripts#0.1.0",
    "com.boscohyun.uniui.examples": "https://github.com/boscohyun/UniUI.git?path=Assets/Plugins/UniUI/Examples#0.1.0",
  }
}
```

## OpenUPM

If you are using openupm, use the command below.

```
> openupm add com.boscohyun.uniui
> openupm add com.boscohyun.uniui.examples
```

