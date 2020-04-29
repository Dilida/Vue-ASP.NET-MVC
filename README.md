# Vue-ASP.NET MVC
* 在MVC內嵌入Vue component，主要給目前無法前後分離MVC環境使用，不單只是CDN import Vue js，
同時也可以import vue component 避免重覆開發。

* 三種不同方式接進值進vue component
    * viewData
    * models
    * call api

### 使用軟體
* Visual Studio 2019 for mac
* Vue.js CLI 4.3.0
* Vue.js 2.6.11

### 建置方式主要參考
https://github.com/sunsande/Vue.js-ASP.NET-MVC-intergration


### vue component import component
* 新增Feature3.vue 做為 import component
```vue
<template>
    <div>
        <p>
            Hello from Feature 3
        </p>
    </div>
</template>
```
* 修改Feature2.vue
```vue
<template>
    <div>
        <F3 />
        <p>
            Hello from Feature 2
        </p>
    </div>
</template>

<script>
    import F3 from './Feature3.vue'
    export default {
        components: {
            F3
        },
    }
</script>

<style>
</style>
```
* 重新執行後可看見Feature 3已經 import to Feature 2
![GITHUB](https://github.com/Dilida/Vue-ASP.NET-MVC/blob/master/img/feature3.png)

### 1.vue component accept the data from viewData
* HomeController.cs 新增回傳值
```asp
public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";
            ViewData["VueData"] = "this is data from vue";

            return View();
        }
```

* Index.cshtml 利用JavaScript 宣告接值
```html
<script>
var vueData = @Html.Raw(Json.Encode(ViewData["VueData"]));
</script>
```

* 在Feature2.vue內將全域vueData接入component data
```vue
<template>
    <div>
        <F3 />
        <p>
            Hello from Feature 2
        </p>
        <p>{{source}}</p>
    </div>
</template>

<script>
    import F3 from './Feature3.vue'
    export default {
        data() {
            return {
                source: vueData
            }  
        },
        components: {
            F3
        },
    }
</script>

<style>
</style>
```
* 切換回到接值頁(index.cshtml)重新執行後即可看到由 viewData 宣告的值
![GITHUB](https://github.com/Dilida/Vue-ASP.NET-MVC/blob/master/img/viewdata.png)


### 2.vue component accept data from model
* 在Model 底下新增檔案 BindingModel.cs
```asp
using System;
namespace vue_mvc.Models
{
    public class BindingModel
    {
        public String message { get; set; }

        public String alert { get; set; }

        public Guid guid { get; set; }

        public BindingModel()
        {

        }
    }
}
```
* 在Controller/HomeController.cs 新增一頁
```asp
public ActionResult Vuetest()
        {
            return View(
               new Models.BindingModel()
               {
                   message = "This is message from model",
                   alert = "Server side message!",
                   guid = Guid.NewGuid()
               });
        }
``` 
* 在Views/Home 底下新增一頁Vuetest.cshtml
```html
<!--換個方式宣告，接進model data-->
<script>
window.serverdata = JSON.parse('@Html.Raw(JsonConvert.SerializeObject(Model))')
</script>
```
* 另外在頁面宣告即將接值的vue component
```html
<script src="~/vuejs/feature4.js"></script>
```
* Feature4.vue相關步驟
 ###### 新增feature4.js
 ```javascript
import Vue from 'vue'
import F4 from './Feature4.vue'

Vue.config.productionTip = false

new Vue({
    render: h => h(F4),
}).$mount('#f4App')
```
###### Feature4.vue 宣告接值
```vue
<template>
    <div>
        <p>
            Hello from Feature 4444444
        </p>
        <ul id="example-4">
            <li>{{serverdata.message}}</li>
            <li>{{serverdata.alert}}</li>
            <li>{{serverdata.guid}}</li>
        </ul>
    </div>
</template>

<script>export default {
        data() {
            return {
                serverdata: window.serverdata
            }
        },
    }</script>

<style>
</style>
```
###### 修改vue.config.js 宣告多出一頁，藉由npm build 執行
```vue
  feature4: {
            entry: 'src/f4.js',
            template: 'public/featureTest4.html',
            filename: 'index4.html',
            title: 'Feature 4',
            chunks: ['chunk-vendors', 'chunk-common', 'feature4']
        }
```

* 切換回到接值頁(Vuetest.cshtml)重新執行後即可看到由 model 宣告的值
![GITHUB](https://github.com/Dilida/Vue-ASP.NET-MVC/blob/master/img/model.png)

### 3.vue call api to get value
* 在controller底下新增HelloWorldController
```asp
        // GET: /HelloWorld/
        public string Index()
        {
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome
        public string Welcome()
        {
            return "welcome api data";
        }
```

* 在Feature3.vue import axios 去call api
```vue
<template>
    <div>
        <p>
            Hello from Feature 3
        </p>
        <button @click="callApi()">click me</button>
        <div>{{data}}</div>
        <div>{{firstView}}</div>

    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                data: null,
                firstView:null,
            }
        },
        mounted() {
            this.callMounted()
        },
        methods: {
            callMounted() {
                axios({
                    method: 'get',
                    baseURL: 'http://127.0.0.1:8080',
                    url: '/Helloworld/Welcome',
                    'Content-Type': 'application/json',
                })
                    .then((result) => { this.firstView = result.data })
                    .catch((err) => { console.error(err) })
            },
            callApi() {
                axios({
                    method: 'get',
                    baseURL: 'http://127.0.0.1:8080',
                    url: '/Helloworld',
                    'Content-Type': 'application/json',
                })
                    .then((result) => {this.data = result.data})
                    .catch((err) => { console.error(err) })
            },
        },
    }
</script>

<style>
</style>
```
