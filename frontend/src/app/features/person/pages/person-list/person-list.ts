import { Component, inject, OnInit, signal } from '@angular/core';
import { Person } from '../../models/person.model';
import { PersonService } from '../../services/person';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-person-list',
  imports: [RouterLink],
  templateUrl: './person-list.html',
  styleUrl: './person-list.css',
})
export class PersonList implements OnInit {
  private personService = inject(PersonService);

  persons = signal<Person[]>([]);
  loading = signal(false);
  searchName = signal('');
  pageNumber = signal(1);
  pageSize = 10;
  hasMoreData = signal(true);
  genderFilter = signal('');
  maritalFilter = signal('');
  graduatedFilter = signal<boolean | undefined>(undefined);

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.loading.set(true);
    this.personService
      .getAll(
        this.searchName(),
        this.genderFilter(),
        this.maritalFilter(),
        this.graduatedFilter(),
        this.pageNumber(),
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.persons.set(res);
          this.loading.set(false);
          this.hasMoreData.set(res.length === this.pageSize);
        },
        error: () => this.loading.set(false),
      });
  }

  search(name: string) {
    this.searchName.set(name);
    this.pageNumber.set(1);
    this.hasMoreData.set(true);
    this.loadData();
  }

  filterGender(value: string) {
    this.genderFilter.set(value);
    this.pageNumber.set(1);
    this.loadData();
  }

  filterMarital(value: string) {
    this.maritalFilter.set(value);
    this.pageNumber.set(1);
    this.loadData();
  }

  filterGraduated(value: string) {
    this.graduatedFilter.set(value === '' ? undefined : value === 'true');
    this.pageNumber.set(1);
    this.loadData();
  }

  nextPage() {
    this.pageNumber.update((p) => p + 1);
    this.loadData();
  }

  prevPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update((p) => p - 1);
      this.loadData();
    }
  }

  delete(id: number) {
    if (!confirm('Are you sure?')) return;
    this.personService.delete(id).subscribe(() => this.loadData());
  }
}
