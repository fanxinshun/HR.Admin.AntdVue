<template>
  <a-select ref="select" :allowClear="allowClear" :showSearch="showSearch" :filterOption="filterOption" :disabled="disableds"
    @search="handleSearch" @change="handleChange" :mode="mode" :placeholder="placeholder" v-model="thisValue">
    <a-icon slot="suffixIcon" :type="suffixIcon" v-if="suffixIcon" />
    <a-select-option v-for="item in thisOptions" :value="item.value" :key="item.value">{{ item.text }}</a-select-option>
  </a-select>
</template>
<script>
  export default {
    props: {
      value: null,
      pageInput: {
        type: Object,
        default: () => {
          return {
            PageIndex: 1,
            PageRows: 30,
            SortField: 'Sort',
            SortType: 'asc',
            Search: null
          }
        }
      },
      url: {
        //远程获取选项接口地址,接口返回数据结构:[{value:'',text:''}]
        type: String,
        default: null
      },
      allowClear: {
        //允许清空
        type: Boolean,
        default: true
      },
      searchMode: {
        //搜索模式,'':关闭搜索,'local':本地搜索,'server':服务端搜索
        type: String,
        default: ''
      },
      options: {
        //下拉项配置,若无url则必选,结构:[{value:'',text:''}]
        type: Array,
        default: () => []
      },
      multiple: {
        type: Boolean,
        default: false
      },
      disableds: {
        type: Boolean,
        default: false
      },
      suffixIcon: {
        type: String,
        default: ''
      },
      placeholder: {
        type: String,
        default: '请输入要查找的内容，默认显示30条记录'
      }
    },
    mounted() {
      this.mode = this.multiple ? 'multiple' : 'default'
      if (this.searchMode) {
        this.showSearch = true
        if (this.searchMode == 'local') {
          this.filterOption = (input, option) => {
            return option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
          }
        } else {
          this.filterOption = false
        }
      }
      if (!this.url && this.options.length > 0) {
        this.thisOptions = this.options
      }
      this.thisValue = this.value
      this.reload()
    },
    data() {
      return {
        filterOption: false, //本地搜索,非远程搜索
        thisOptions: [],
        mode: '',
        showSearch: false,
        isInnerchange: false,
        thisValue: '',
        timeout: null,
        qGlobal: ''
      }
    },
    watch: {
      value(value) {
        this.thisValue = value
      }
    },
    methods: {
      reload(q) {
        if (!this.url) {
          return
        }
        this.qGlobal = q
        clearTimeout(this.timeout)
        this.timeout = setTimeout(() => {
          let selected = []
          if (this.multiple) {
            selected = this.$refs.select.value
          }
          this.$http
            .post(this.url, {
              pageInput: this.pageInput,
              q: q || '',
              selectedValues: selected || []
            })
            .then((resJson) => {
              if (resJson.Success && q == this.qGlobal) {
                this.thisOptions = resJson.Data
              }
            })
        }, 300)
      },
      handleSearch(value) {
        this.reload(value)
      },
      handleChange(value) {
        this.$emit('input', value)
        this.$emit('handleInput', {
          value: value,
          thisOptions: this.thisOptions
        })
      },
    }
  }
</script>
