<template>
  <div class="tela">
    <h2>Viagem</h2>

    <form @submit.prevent="salvar">
      <input v-model="form.nome" type="text" placeholder="Nome" required />
      <input v-model="form.dataChegada" type="date" required />
      <input v-model="form.dataSaida" type="date" required />
      <input v-model.number="form.valor" type="number" step="0.01" placeholder="Valor" required />

      <button type="submit">
        {{ editando ? "Atualizar" : "Salvar" }}
      </button>

      <button v-if="editando" type="button" @click="limpar">
        Cancelar
      </button>
    </form>

    <ul class="lista">
      <li v-for="item in viagens" :key="item.id" class="linha">
        <div class="info">
          <strong>{{ item.nome }}</strong>

          <span class="datas">
            {{ formatarData(item.dataChegada) }} até {{ formatarData(item.dataSaida) }}
          </span>

          <span class="valor">
            R$ {{ item.valor.toLocaleString("pt-BR") }}
          </span>
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
import { reactive, ref, onMounted } from "vue";
import type { Viagem } from "../types/Viagem";
import { viagemService } from "../services/viagemService";

const emit = defineEmits(["atualizado"]);

const viagens = ref<Viagem[]>([]);
const editando = ref(false);
const idEdicao = ref("");

const form = reactive<Viagem>({
  nome: "",
  dataChegada: "",
  dataSaida: "",
  valor: 0,
});

async function carregar() {
  try {
    const data = await viagemService.getAll();
    viagens.value = data;
  } catch (error: any) {
    console.error("Erro ao carregar viagens:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta:", error?.response?.data);
  }
}

async function salvar() {
  try {
    const payload = {
      nome: form.nome,
      dataChegada: `${form.dataChegada}T00:00:00`,
      dataSaida: `${form.dataSaida}T00:00:00`,
      valor: Number(form.valor),
    };

    if (editando.value) {
      await viagemService.update(idEdicao.value, payload as Viagem);
    } else {
      await viagemService.create(payload as Viagem);
    }

    await carregar();
    emit("atualizado");
    limpar();
  } catch (error: any) {
    console.error("Erro completo:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta da API:", error?.response?.data);
  }
}

function editar(item: Viagem) {
  form.nome = item.nome;
  form.dataChegada = item.dataChegada?.split("T")[0] ?? item.dataChegada;
  form.dataSaida = item.dataSaida?.split("T")[0] ?? item.dataSaida;
  form.valor = item.valor;

  idEdicao.value = item.id || "";
  editando.value = true;
}

async function remover(id: string) {
  try {
    await viagemService.delete(id);
    await carregar();
    emit("atualizado");
  } catch (error: any) {
    console.error("Erro ao remover viagem:", error);
    console.error("Status:", error?.response?.status);
    console.error("Resposta:", error?.response?.data);
  }
}

function limpar() {
  form.nome = "";
  form.dataChegada = "";
  form.dataSaida = "";
  form.valor = 0;

  idEdicao.value = "";
  editando.value = false;
}

function formatarData(data: string) {
  return new Date(data).toLocaleDateString("pt-BR");
}

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

.valor {
  font-weight: bold;
  color: #4caf50;
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