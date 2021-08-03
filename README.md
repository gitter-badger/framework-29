<h1 align="center">OneF</h1>

> `One Framework`，一个基础设施框架，使用CSharp语言

## 目标

[![Join the chat at https://gitter.im/one-gitter-community/framework](https://badges.gitter.im/one-gitter-community/framework.svg)](https://gitter.im/one-gitter-community/framework?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

- 尽量减少代码侵入，使用`Attribute`来达到框架的目的
- 会被外部引用的实现，抽象类，需加前缀`OneF`
- 框架的行为尽可能实现可通过配置更改

## 小灯泡

- app层不应该关注任何通信协议的细节，他所展示的是用户用例，而协议或可通过Attribute来实现
- 多租户，实体不需要加`TenantId`，因为一般不会在代码中使用到`TenantId`，只需获取`urrentTenantId`

## 增强

- 热加载dll

## 流程

功能+测试 > 文档 > UI（ui方面的修改，只作为补充）

## 参考框架

- `abp`: <https://github.com/abpframework/abp>
- `furion`: <https://dotnetchina.gitee.io/furion/docs>
