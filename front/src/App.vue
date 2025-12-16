<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'

const API_URL = 'http://localhost:5225'

interface Book {
  isbn: string
  title: string
  AvailableAmount: number
}

interface LoanResponse {
  id: string
  maxDevolutionDate: string
}

const book: Book = {
  isbn: '',
  title: '',
  AvailableAmount: 1,
}

const loan = ref({
  isbn: '',
  userId: '',
  typeUser: 1,
})

const message = ref<{
  text: string
  type: 'success' | 'error' | 'info'
} | null>(null)

const loading = ref(false)
const activeForm = ref<'book' | 'loan'>('book')

const createBook = async () => {
  loading.value = true
  message.value = null

  try {
    await axios.post(`${API_URL}/api/books`, book)
    message.value = {
      text: '📘 Libro creado correctamente',
      type: 'success',
    }
  } catch (e: any) {
    message.value = {
      text: e.response?.data?.message || '❌ Error al crear el libro',
      type: 'error',
    }
  } finally {
    loading.value = false
  }
}

const createLoan = async () => {
  loading.value = true
  message.value = null

  try {
    const res = await axios.post<LoanResponse>(`${API_URL}/api/loan`, loan.value)
    message.value = {
      text: `📅 Préstamo creado. Devolución máxima: ${new Date(
        res.data.maxDevolutionDate,
      ).toLocaleString()}`,
      type: 'success',
    }
  } catch (e: any) {
    message.value = {
      text: e.response?.data?.message || '❌ Error al crear el préstamo',
      type: 'error',
    }
  } finally {
    loading.value = false
  }
}
</script>
<template>
  <div class="container">
    <h1>📚 Indigo Library</h1>

    <div class="form-selector">
      <button :class="{ active: activeForm === 'book' }" @click="activeForm = 'book'">
        📘 Agregar Libro
      </button>

      <button :class="{ active: activeForm === 'loan' }" @click="activeForm = 'loan'">
        🔄 Crear Préstamo
      </button>
    </div>

    <div class="forms-wrapper">
      <transition name="fade">
        <section v-show="activeForm === 'book'" class="card form">
          <h2>Agregar Libro</h2>
          <input v-model="book.isbn" placeholder="ISBN" />
          <input v-model="book.title" placeholder="Título" />
          <input v-model.number="book.AvailableAmount" type="number" placeholder="Cantidad" />
          <button :disabled="loading" @click="createBook">
            {{ loading ? 'Creando...' : 'Crear Libro' }}
          </button>
        </section>
      </transition>

      <transition name="fade">
        <section v-show="activeForm === 'loan'" class="card form">
          <h2>Crear Préstamo</h2>
          <input v-model="loan.isbn" placeholder="ISBN Libro" />
          <input v-model="loan.userId" placeholder="ID Usuario" />
          <select v-model.number="loan.typeUser">
            <option :value="1">Afiliado</option>
            <option :value="2">Empleado</option>
            <option :value="3">Invitado</option>
          </select>
          <button :disabled="loading" @click="createLoan">
            {{ loading ? 'Procesando...' : 'Crear Préstamo' }}
          </button>
        </section>
      </transition>
    </div>

    <!-- 👇 Renderizado condicional -->
    <transition name="fade">
      <p v-if="message" class="message" :class="message.type">
        {{ message.text }}
      </p>
    </transition>
  </div>
</template>
<style scoped>
.container {
  max-width: 600px;
  margin: auto;
  font-family: system-ui, sans-serif;
}

h1 {
  text-align: center;
}

.card {
  background: #ffffff;
  padding: 1rem;
  margin-bottom: 1rem;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.08);
}

.forms-wrapper {
  position: relative;
  width: 100%;
  min-height: 320px;
}

.form {
  position: absolute;
  inset: 0;
  width: 100%;
}


.form-selector {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.form-selector button {
  flex: 1;
  padding: 0.6rem;
  border-radius: 8px;
  border: 1px solid #42b883;
  background: white;
  color: #42b883;
  font-weight: 600;
}

.form-selector button.active {
  background: #42b883;
  color: white;
}

input,
select {
  display: block;
  width: 100%;
  margin-bottom: 0.5rem;
  padding: 0.5rem;
}

button {
  width: 100%;
  padding: 0.6rem;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

button:disabled {
  background: #9dd6bd;
  cursor: not-allowed;
}

.message {
  margin-top: 1rem;
  padding: 0.75rem;
  border-radius: 6px;
  text-align: center;
  font-weight: 600;
}

.message.success {
  background: #e6f7f0;
  color: #1f7a5c;
}

.message.error {
  background: #fdecea;
  color: #b42318;
}

.message.info {
  background: #eef4ff;
  color: #1d4ed8;
}

/* animación */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
