# hash
hash is a cryptographic Hashing components that greatly simplify one-way data hashing in an application, deep inspired from [Apache Shiro SimpleHash ](http://shiro.apache.org/static/1.2.1/apidocs/org/apache/shiro/crypto/hash/SimpleHash.html).

## goal
There are a lot of complaims about different result between .NET hashing and Java hashing, while Apache Shiro SimpleHash compute hash with salt and iterations, that's sound more reasonable than that it's no same concept in .NET.  


Recently I got several customer requests which compute same hashing between Apache Shiro Hash library and .NET app, so they migrate all Java code to .NET, after some research, I decide wrappe the SimpleHash in .NET.

## usage
```c#
using GrapeCity.Crypto.Hash;
//...
// 
var hash = new SimpleHash ("SHA1", "source data", "salt", 10);
//// above line equals to
// var hash = new Sha1Hash("source data", "salt", 10);
var hexEncoded = hash.ToHex();
// result:
// 0b062d3b6ce15bdf91fb281c012ff05c9b70a39d
var base64Encoded = hash.ToBase64();
// result:
// CwYtO2zhW9+R+ygcAS/wXJtwo50=
```

## supported hash algorithms
* MD2
* MD5
* SHA-1
* SHA-256
* SHA-384
* SHA-512

## license - MIT
The MIT License (MIT)

Copyright (c) 2016 Zhang Wenqing (winking.zhang@grapecity.com), GrapeCity inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
