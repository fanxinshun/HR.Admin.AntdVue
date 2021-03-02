<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete(selectedRowKeys)" :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <!--    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="type_name">模板类型名称</a-select-option>
                <a-select-option key="updated_user">修改人</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="() => {this.pagination.current = 1; this.getDataList()}">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div> -->

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true" size="small">
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
      <span slot="template" slot-scope="text, record">
        <template>
          <c-upload-file v-model="entity.File" :maxCount="1" :fileClassified="fileClassified"></c-upload-file>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
  import CUploadFile from '@/components/CUploadFile/CUploadFile'
  import EditForm from './EditForm'

  const columns = [{
      title: '模板类型编号',
      dataIndex: 'type_code',
      width: '165px'
    },
    {
      title: '模板类型名称',
      dataIndex: 'type_name'
    },
    {
      title: '创建人',
      dataIndex: 'CreatorId'
    },
    {
      title: '创建时间',
      dataIndex: 'CreateTime'
    },
    {
      title: '修改时间',
      dataIndex: 'updated_time'
    },
    {
      title: '操作',
      dataIndex: 'action',
      scopedSlots: {
        customRender: 'action'
      }
    },
    {
      title: '上传模板',
      dataIndex: 'template',
      scopedSlots: {
        customRender: 'template'
      },
      width: '180px'
    }
  ]

  export default {
    components: {
      CUploadFile,
      EditForm
    },
    mounted() {
      this.getDataList()
    },
    data() {
      return {
        entity: {
          File: ''
        },
        data: [],
        pagination: {
          current: 1,
          pageSize: 10,
          showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
        },
        filters: {},
        sorter: {
          field: 'Id',
          order: 'asc'
        },
        loading: false,
        columns,
        queryParam: {},
        selectedRowKeys: [],
        fileClassified: 'HrAssessment'
      }
    },
    methods: {
      handleTableChange(pagination, filters, sorter) {
        this.pagination = { ...pagination
        }
        this.filters = { ...filters
        }
        this.sorter = { ...sorter
        }
        this.getDataList()
      },
      getDataList() {
        let app = this
        app.selectedRowKeys = []

        app.loading = true
        app.$http
          .post('/HrAssessment/hr_assessment_template_type/GetDataList', {
            PageIndex: app.pagination.current,
            PageRows: app.pagination.pageSize,
            SortField: app.sorter.field || 'Id',
            SortType: app.sorter.order,
            Search: app.queryParam,
            ...app.filters
          })
          .then(resJson => {
            app.loading = false
            app.data = resJson.Data
            const pagination = { ...app.pagination
            }
            pagination.total = resJson.Total
            app.pagination = pagination
          })
      },
      onSelectChange(selectedRowKeys) {
        this.selectedRowKeys = selectedRowKeys
      },
      hasSelected() {
        return this.selectedRowKeys.length > 0
      },
      hanldleAdd() {
        this.$refs.editForm.openForm()
      },
      handleEdit(id) {
        this.$refs.editForm.openForm(id)
      },
      handleEditTemplate(id) {
        this.$refs.editForm.openForm(id)
      },
      handleDelete(ids) {
        var thisObj = this
        this.$confirm({
          title: '确认删除吗?',
          onOk() {
            return new Promise((resolve, reject) => {
              thisObj.$http.post('/HrAssessment/hr_assessment_template_type/DeleteData', ids).then(resJson => {
                resolve()

                if (resJson.Success) {
                  thisObj.$message.success('操作成功!')

                  thisObj.getDataList()
                } else {
                  thisObj.$message.error(resJson.Msg)
                }
              })
            })
          }
        })
      }
    }
  }
</script>
