<template>
  <div>
    <h2>Destino</h2>

    <form @submit.prevent="salvar">
      <input v-model="form.nome" type="text" placeholder="Nome do destino" required />

      <select v-model="form.viagemId" required>
        <option value="">Selecione uma viagem</option>
        <option v-for="viagem in viagens" :key="viagem.id" :value="viagem.id">
          {{ viagem.nome }}
        </option>
      </select>

      <button type="submit">
        {{ editando ? "Atualizar" : "Salvar" }}
      </button>

      <button v-if="editando" type="button" @click="limpar">
        Cancelar
      </button>
    </form>

    <ul class="lista">
      <li v-for="item in destinos" :key="item.id" class="linha">
        <div class="info">
          <strong>{{ item.nome }}</strong>
          <span class="datas">Viagem: {{ nomeViagem(item.viagemId) }}</span>
        </div>

        <div class="acoes">
          <button @click="editar(item)">Editar</button>
          <button class="danger" @click="remover(item.id!)">Excluir</button>
        </div>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted, watch } from "vue";
import type { Destino } from "../types/Destino";
import type { Viagem } from "../types/Viagem";
import { destinoService } from "../services/destinoService";
import { viagemService } from "../services/viagemService";

const props = defineProps<{
  refreshKey: number;
}>();

const destinos = ref<Destino[]>([]);
const viagens = ref<Viagem[]>([]);
const editando = ref(false);
const idEdicao = ref("");

const form = reactive<Destino>({
  nome: "",
  viagemId: "",
});

async function carregar() {
  try {
    destinos.value = await destinoService.getAll();
    viagens.value = await viagemService.getAll();
  } catch (error: any) {
    console.error("Erro ao carregar destinos:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta:", error?.response?.data);
  }
}

async function salvar() {
  try {
    if (editando.value) {
      await destinoService.update(idEdicao.value, { ...form });
    } else {
      await destinoService.create({ ...form });
    }

    limpar();
    await carregar();
  } catch (error: any) {
    console.error("Erro ao salvar destino:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta:", error?.response?.data);
  }
}

function editar(item: Destino) {
  form.nome = item.nome;
  form.viagemId = item.viagemId;

  idEdicao.value = item.id || "";
  editando.value = true;
}

async function remover(id: string) {
  try {
    await destinoService.delete(id);
    await carregar();
  } catch (error: any) {
    console.error("Erro ao remover destino:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta:", error?.response?.data);
  }
}

function limpar() {
  form.nome = "";
  form.viagemId = "";

  idEdicao.value = "";
  editando.value = false;
}

function nomeViagem(viagemId: string) {
  const viagem = viagens.value.find((v) => v.id === viagemId);
  return viagem ? viagem.nome : "Não encontrada";
}

watch(() => props.refreshKey, () => {
  carregar();
});

onMounted(carregar);
</script>

<style scoped>
form {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
  margin-bottom: 20px;
}

input,
select,
button {
  padding: 8px;
}

.lista {
  list-style: none;
  padding: 0;
  margin-top: 20px;
}

.linha {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px;
  margin-bottom: 8px;
  background: #1e1e1e;
  border-radius: 6px;
}

.info {
  display: flex;
  color: white;
  flex-direction: column;
  gap: 4px;
}

.datas {
  font-size: 14px;
  color: #aaa;
}

.acoes {
  display: flex;
  gap: 8px;
}

button {
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  opacity: 0.9;
}

.danger {
  background: #c62828;
  color: white;
}
</style>