<template>
  <div class="tela">
    <h1 class="forms">Formulários</h1>
    <ViagemForm @atualizado="recarregarChave++" />
    <hr />
    <DestinoForm :refreshKey="recarregarChave" />
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import ViagemForm from "./views/ViagemForm.vue";
import DestinoForm from "./views/DestinoForm.vue";

const recarregarChave = ref(0);
</script>

<style scoped>
  .forms{
    border-radius: 6px;
    text-align: center;
    background-color: #1e1e1e;
    color: white;
    padding: 6px;
  }

  .tela{
    background-color: rgb(129, 119, 119);
    border-radius: 6px;
  }
</style>